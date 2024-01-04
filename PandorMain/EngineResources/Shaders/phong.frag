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
uniform sampler2D shadowMap;
uniform bool enableTexture;
uniform bool enableNormalMap;
uniform vec3 viewPos;

uniform int LightShader;

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
struct State
{
    vec3 normal;
    vec3 viewDir;

    vec3 albedo;
};
State state;

// ----------------------------------------------------------------------------
void SetState()
{
    state.albedo = vec3(1, 1, 1);
    if (enableTexture)
        state.albedo = pow(texture(albedoMap, texCoord).rgb, vec3(2.2));

    state.normal = normalize(normal);  
    if (enableNormalMap)
        state.normal = CalcBumpedNormal();

    state.viewDir = normalize(viewPos - fragPos);
}

float ShadowCalculation(vec4 fragPosLightSpace)
{
    // perform perspective divide
    vec3 projCoords = fragPosLightSpace.xyz / fragPosLightSpace.w;
    projCoords = projCoords * 0.5 + 0.5; 
    float closestDepth = texture(shadowMap, projCoords.xy).r;   
    float currentDepth = projCoords.z;  
    float shadow = currentDepth > closestDepth  ? 1.0 : 0.0; 

    return shadow;
}

float near = 0.1; 
float far  = 100.0; 

float LinearizeDepth(float depth) 
{
    float z = depth * 2.0 - 1.0; // back to NDC 
    return (2.0 * near * far) / (far + near - z * (far - near));	
}

// ----------------------------------------------------------------------------
vec3 CalcDirLight(DirLight light)
{
    vec3 lightDir = normalize(-light.direction);

    float diff = max(dot(state.normal, lightDir), 0.0);

    vec3 reflectDir = reflect(-lightDir, state.normal);
    float spec = pow(max(dot(state.viewDir, reflectDir), 0.0), 32);

    vec3 radiance = state.albedo * light.color;
    vec3 diffuse  = radiance * material.diffuse.xyz * diff;
    vec3 specular = radiance * material.specular.xyz * spec;

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

    return (1.0 - shadow) * (diffuse + specular) * light.intensity;
}  

// ----------------------------------------------------------------------------
vec3 CalcPointLight(PointLight light)
{
    float distance = length(light.position - fragPos);
    if(distance > light.radius)
        return vec3(0.0);

    float attenuationValue = 1.0 / (attenuation.constant + attenuation.linear * distance + attenuation.quadratic * (distance * distance));
    float radiusIntensity = 1.0 - smoothstep(light.radius - 0.2 * light.radius, light.radius, distance);

    vec3 lightDir = normalize(light.position - fragPos);
    float diff = max(dot(state.normal, lightDir), 0.0);
    vec3 reflectDir = reflect(-lightDir, state.normal);
    float spec = pow(max(dot(state.viewDir, reflectDir), 0.0), 32);

    vec3 radiance = state.albedo * light.color * attenuationValue;
    vec3 diffuse  = radiance * material.diffuse.xyz * diff;
    vec3 specular = radiance * material.specular.xyz * spec;

    return (diffuse + specular) * light.intensity * radiusIntensity;
}

// ----------------------------------------------------------------------------
vec3 CalcSpotLight(SpotLight light)
{
    
    vec3 lightDir = normalize(light.position - fragPos);
    float theta = dot(lightDir, normalize(-light.direction)); 
    float epsilon = light.cutOff - light.outerCutOff;
    float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);
    
    if(theta > light.outerCutOff) 
    {   
        float diff = max(dot(state.normal, lightDir), 0.0);

        vec3 reflectDir = reflect(-lightDir, state.normal);
        float spec = pow(max(dot(state.viewDir, reflectDir), 0.0), 32);

        float distance = length(light.position - fragPos);
        float attenuationValue = 1.0 / (attenuation.constant + attenuation.linear * distance + attenuation.quadratic * (distance * distance));

        vec3 radiance = state.albedo * light.color * intensity * attenuationValue;
        vec3 diffuse  = radiance * material.diffuse.xyz * diff;
        vec3 specular = radiance * material.specular.xyz * spec;

        return (diffuse + specular) * light.intensity;
    }
    return vec3(0.0);
}

// ----------------------------------------------------------------------------
void main()
{
    SetState();

    vec3 result = vec3(0.0);
    bool lightActive = false;
    bool tmp = lightEnable;

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
        vec3 ambient = vec3(0.0001) * state.albedo;
        vec3 color = material.ambient.xyz * ambient + result;
        // HDR tonemapping
        color = color / (color + vec3(1.0));
        // gamma correct
        color = pow(color, vec3(1.0/2.2)); 

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

