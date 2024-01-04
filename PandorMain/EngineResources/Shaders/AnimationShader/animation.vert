#version 330 core
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTex;
layout(location = 2) in vec3 aNor;
layout(location = 3) in vec3 aTan;
layout(location = 4) in vec4 boneIndices;
layout(location = 5) in vec4 boneIndices2;
layout(location = 6) in vec4 boneWeights;
layout(location = 7) in vec4 boneWeights2;

out vec2 texCoord;
out vec3 normal;
out vec3 fragPos;
out vec3 tangent;
out vec4 color;
out vec4 fragPosLight;

uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;
uniform int MaxBoneWeight;
const int MAX_BONES = 100;
uniform mat4 skinningMatrices[MAX_BONES];

void main()
{
    vec4 worldPosition = vec4(0.0f);
    vec4 normalOut = vec4(0.0f);
    vec4 tangentOut = vec4(0.0f);

    vec4 pos = vec4(aPos, 1.0f);
    
    for (int i = 0; i < 4; i++)
    {
        worldPosition += boneWeights[i] * (pos * skinningMatrices[int(boneIndices[i])]);
        normalOut += boneWeights[i] * (vec4(aNor, 0.f) * skinningMatrices[int(boneIndices[i])]);
        tangentOut += boneWeights[i] * (vec4(aTan, 0.f) * skinningMatrices[int(boneIndices[i])]);
    }
    
    for (int i = 0; i < 4; i++)
    {
        worldPosition += boneWeights2[i] * (pos * skinningMatrices[int(boneIndices2[i])]);
        normalOut += boneWeights2[i] * (vec4(aNor, 0.f) * skinningMatrices[int(boneIndices2[i])]);
        tangentOut += boneWeights2[i] * (vec4(aTan, 0.f) * skinningMatrices[int(boneIndices2[i])]);
    }
    
    // Transform the vertex position to clip space
    gl_Position = (worldPosition * viewMatrix) * projectionMatrix;
    
    // Output the texture coordinates and normal for the fragment shader
    texCoord = aTex;
    normal = normalize(normalOut.xyz);
    fragPos = vec3(vec4(aPos, 1.0) * modelMatrix);
    tangent = (modelMatrix * vec4(normalize(tangentOut.xyz), 0)).xyz;
    color = vec4(1, 1, 1, 1);
}
