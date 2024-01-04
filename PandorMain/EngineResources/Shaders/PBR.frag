#version 330 core
out vec4 FragColor;

in vec2 texCoord;
in vec3 normal;
in vec3 fragPos;
in vec3 tangent;
in vec4 color;
in vec4 fragPosLight;

uniform sampler2D albedoMap;
uniform sampler2D normalMap;
uniform sampler2D roughnessMap;
uniform sampler2D metallicMap;
uniform samplerCube skybox;
uniform sampler2D shadowMap;
uniform bool enableTexture;
uniform bool enableNormalMap;
uniform bool enableRoughnessMap;
uniform bool enableMetallicMap;
uniform vec3 viewPos;

uniform int LightShader;

const float PI = 3.14159265359;

// ----------------------------------------------------------------------------
struct Attenuation
{
    float constant;
    float linear;
    float quadratic; 
};
const Attenuation attenuation = Attenuation(1.0, 0.09, 0.032);

// ----------------------------------------------------------------------------
struct DirLight {
    bool enable;
    float intensity;
    vec3 color;

    vec3 direction;
};

#define MAX_LIGHTS 25

uniform DirLight dirLights[MAX_LIGHTS];
uniform int sizeDirLights;

// ----------------------------------------------------------------------------
struct PointLight {
    bool enable;
    float intensity;
    vec3 color;

    vec3 position;
    float radius;
};
uniform PointLight pointLights[MAX_LIGHTS];
uniform int sizePointLights;

// ----------------------------------------------------------------------------
struct SpotLight { 
    bool enable;
    float intensity;
    vec3 color;

    vec3  position;
    vec3  direction;

    float cutOff;
    float outerCutOff;
}; 

uniform SpotLight spotLights[MAX_LIGHTS];
uniform int sizeSpotLights;

// ----------------------------------------------------------------------------
struct Material
{
    vec4 ambient;
    vec4 diffuse;
    vec4 specular;
};

uniform Material material;

uniform bool lightEnable = true;
// ----------------------------------------------------------------------------
vec3 CalcBumpedNormal()
{
	vec3 norm = normalize(normal);
	vec3 tang = normalize(tangent);
	// Re-orthogonalize tangent.
	tang = normalize(tang - dot(tang, norm) * norm);
	vec3 Bitangent = cross(tang, norm);
	vec3 BumpMapNormal = texture(normalMap, texCoord).xyz;
	BumpMapNormal = 2.0 * BumpMapNormal - vec3(1.0, 1.0, 1.0);
	vec3 NewNormal;
	mat3 TBN = mat3(tang, Bitangent, norm);
	NewNormal = TBN * BumpMapNormal;
	NewNormal = normalize(NewNormal);
	return NewNormal;
}

// ----------------------------------------------------------------------------
float DistributionGGX(vec3 N, vec3 H, float roughness)
{
    float a = roughness*roughness;
    float a2 = a*a;
    float NdotH = max(dot(N, H), 0.0);
    float NdotH2 = NdotH*NdotH;

    float nom   = a2;
    float denom = (NdotH2 * (a2 - 1.0) + 1.0);
    denom = PI * denom * denom;

    return nom / denom;
}

// ----------------------------------------------------------------------------
float GeometrySchlickGGX(float NdotV, float roughness)
{
    float r = (roughness + 1.0);
    float k = (r*r) / 8.0;

    float nom   = NdotV;
    float denom = NdotV * (1.0 - k) + k;

    return nom / denom;
}

// ----------------------------------------------------------------------------
float GeometrySmith(vec3 N, vec3 V, vec3 L, float roughness)
{
    float NdotV = max(dot(N, V), 0.0);
    float NdotL = max(dot(N, L), 0.0);
    float ggx2 = GeometrySchlickGGX(NdotV, roughness);
    float ggx1 = GeometrySchlickGGX(NdotL, roughness);

    return ggx1 * ggx2;
}

// ----------------------------------------------------------------------------
vec3 fresnelSchlick(float cosTheta, vec3 F0)
{
    return F0 + (1.0 - F0) * pow(clamp(1.0 - cosTheta, 0.0, 1.0), 5.0);
}

// ----------------------------------------------------------------------------
struct State
{
    vec3 normal;
    vec3 viewDir;

    vec3 albedo;
    float roughness;
    float metallic;
    vec3 F0;
};

State state;
uniform float roughnessValue;
uniform float metallicValue;

void SetState()
{
    state.albedo = vec3(1, 1, 1);
    if (enableTexture)
        state.albedo = pow(texture(albedoMap, texCoord).rgb, vec3(2.2));

    state.roughness = roughnessValue;
    if (enableRoughnessMap)
        state.roughness = texture(roughnessMap, texCoord).r;
    
    state.metallic = metallicValue;
    if (enableMetallicMap)
        state.metallic = texture(metallicMap, texCoord).r;

    state.normal = normalize(normal);  
    if (enableNormalMap)
        state.normal = CalcBumpedNormal();

    state.viewDir = normalize(viewPos - fragPos);

    state.F0 = vec3(0.04); 
    state.F0 = mix(state.F0, state.albedo, state.metallic);
}

// ----------------------------------------------------------------------------
vec3 CalcDirLight(DirLight light)
{
    vec3 lightDir = normalize(-light.direction);
    vec3 H = normalize(state.viewDir + lightDir);

    // Cook-Torrance BRDF
    float NDF = DistributionGGX(state.normal, H, state.roughness);   
    float G   = GeometrySmith(state.normal, state.viewDir, lightDir, state.roughness);      
    vec3 F    = fresnelSchlick(max(dot(H, state.viewDir), 0.0), state.F0);
           
    vec3 numerator    = NDF * G * F; 
    float denominator = 4.0 * max(dot(state.normal, state.viewDir), 0.0) * max(dot(state.normal, lightDir), 0.0) + 0.0001; // + 0.0001 to prevent divide by zero
    vec3 specular = numerator / denominator;

    vec3 kS = F;
    vec3 kD = vec3(1.0) - kS;
    kD *= 1.0 - state.metallic;

    float NdotL = max(dot(state.normal, lightDir), 0.0);

    // Shadow value
	float shadow = 0.0f;
	// Sets lightCoords to cull space
	vec3 lightCoords = fragPosLight.xyz / fragPosLight.w;

	if(lightCoords.z <= 1.0f)
	{
		float currentDepth = lightCoords.z;
        // Get from [-1, 1] range to [0, 1] range just like the shadow map
    	lightCoords = (lightCoords + 1.0f) / 2.0f;
		// Prevents shadow acne
		float bias = max(0.025f * (1.0f - dot(normal, lightDir)), 0.0005f);

		// Smoothens out the shadows
		int sampleRadius = 2;
		vec2 pixelSize = 1.0 / textureSize(shadowMap, 0);
		for(int y = -sampleRadius; y <= sampleRadius; y++)
		{
		    for(int x = -sampleRadius; x <= sampleRadius; x++)
		    {
		        float closestDepth = texture(shadowMap, lightCoords.xy + vec2(x, y) * pixelSize).r;
				if (currentDepth > closestDepth + bias)
					shadow += 1.0f;     
		    }    
		}
		// Get average shadow
		shadow /= pow((sampleRadius * 2 + 1), 2);

	}

    return (1.0 - shadow) * ((material.diffuse.xyz * (kD * state.albedo / PI * NdotL)) + (material.specular.xyz * specular * light.color * NdotL * light.intensity));
} 

// ----------------------------------------------------------------------------
vec3 CalcPointLight(PointLight light)
{
    float distance = length(light.position - fragPos);
    if(distance > light.radius)
        return vec3(0.0);

    vec3 lightDir = normalize(light.position - fragPos);
    vec3 H = normalize(state.viewDir + lightDir);

    float attenuation = 1.0 / (attenuation.constant + attenuation.linear * distance + attenuation.quadratic * (distance * distance));
    float radiusIntensity = 1.0 - smoothstep(light.radius - 0.2 * light.radius, light.radius, distance);
    vec3 radiance = light.color * attenuation;

    // Cook-Torrance BRDF
    float NDF = DistributionGGX(state.normal, H, state.roughness);   
    float G   = GeometrySmith(state.normal, state.viewDir, lightDir, state.roughness);      
    vec3 F    = fresnelSchlick(max(dot(H, state.viewDir), 0.0), state.F0);
           
    vec3 numerator    = NDF * G * F; 
    float denominator = 4.0 * max(dot(state.normal, state.viewDir), 0.0) * max(dot(state.normal, lightDir), 0.0) + 0.0001; // + 0.0001 to prevent divide by zero
    vec3 specular = numerator / denominator;

    vec3 kS = F;
    vec3 kD = vec3(1.0) - kS;
    kD *= 1.0 - state.metallic;

    float NdotL = max(dot(state.normal, lightDir), 0.0);

    return ((material.diffuse.xyz * (kD * state.albedo / PI)) + (material.specular.xyz * specular)) * radiance * NdotL * light.intensity * radiusIntensity;
}

// ----------------------------------------------------------------------------
vec3 CalcSpotLight(SpotLight light)
{
    
    vec3 lightDir = normalize(light.position - fragPos);
    float theta = dot(lightDir, normalize(-light.direction)); 
    
    if(theta > light.outerCutOff) 
    {   
        float epsilon = light.cutOff - light.outerCutOff;
        float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);

         vec3 H = normalize(state.viewDir + lightDir);

        float distance = length(light.position - fragPos);
        float attenuation = 1.0 / (attenuation.constant + attenuation.linear * distance + attenuation.quadratic * (distance * distance));
        vec3 radiance = light.color * attenuation * intensity;

        // Cook-Torrance BRDF
        float NDF = DistributionGGX(state.normal, H, state.roughness);   
        float G   = GeometrySmith(state.normal, state.viewDir, lightDir, state.roughness);      
        vec3 F    = fresnelSchlick(max(dot(H, state.viewDir), 0.0), state.F0);
           
        vec3 numerator    = NDF * G * F; 
        float denominator = 4.0 * max(dot(state.normal, state.viewDir), 0.0) * max(dot(state.normal, lightDir), 0.0) + 0.0001; // + 0.0001 to prevent divide by zero
        vec3 specular = numerator / denominator;

        vec3 kS = F;
        vec3 kD = vec3(1.0) - kS;
        kD *= 1.0 - state.metallic;

        float NdotL = max(dot(state.normal, lightDir), 0.0);

        return ((material.diffuse.xyz * (kD * state.albedo / PI)) + (material.specular.xyz * specular)) * radiance * NdotL * light.intensity;  
    }
    return vec3(0.0);
}

// ----------------------------------------------------------------------------
void main()
{
    SetState();
    bool tmp = lightEnable;
    vec3 result = vec3(0.0);
    bool lightActive = false;

    //directional light
    for(int i = 0; i < sizeDirLights; i++)
    {
        if (!dirLights[i].enable)
            continue;
        result = CalcDirLight(dirLights[i]);
        lightActive = true;
    }

    //point lights
    for(int i = 0; i < sizePointLights; i++)
    {
        if (!pointLights[i].enable)
            continue;
        result += CalcPointLight(pointLights[i]);
        lightActive = true;
    }

    //spot light
    for(int i = 0; i < sizeSpotLights; i++)
    {
        if (!spotLights[i].enable)
            continue;
        result += CalcSpotLight(spotLights[i]);
        lightActive = true;
    }

    if(lightActive)
    {
        vec3 ambient = vec3(0.03) * state.albedo;
        vec3 color = material.ambient.xyz * ambient + result;
        // HDR tonemapping
        color = color / (color + vec3(1.0));
        // gamma correct
        color = pow(color, vec3(1.0/2.2)); 

        // Skybox reflection
        if (state.metallic > 0) {	
	        vec3 view = normalize(viewPos - fragPos);
            vec3 R = reflect(-view, normalize(normal));
		    color += vec3(texture(skybox, R).rgb * state.metallic);
        }

        FragColor = vec4(color, 1.0);
    }
    else
    {
        if (enableTexture) 
            FragColor = texture2D(albedoMap, texCoord) * material.diffuse;
        else 
            FragColor = material.diffuse;
    }
}
