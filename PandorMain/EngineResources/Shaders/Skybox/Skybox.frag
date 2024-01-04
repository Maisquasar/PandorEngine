#version 330 core
out vec4 FragColor;

in vec3 fragPos;
in vec2 texCoord;
in vec3 normal;
in vec3 tangent;
in vec4 color;
in vec4 fragPosLight;

uniform samplerCube skybox;

void main()
{    
    FragColor = texture(skybox, fragPos);
}