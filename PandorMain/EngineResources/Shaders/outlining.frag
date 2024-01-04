#version 330

out vec4 FragColor;
uniform vec3 OutlineColor;

in vec2 texCoord;
in vec3 normal;
in vec3 fragPos;
in vec3 tangent;
in vec4 color;
in vec4 fragPosLight;

void main()
{
    FragColor = vec4(OutlineColor.x, OutlineColor.y, OutlineColor.z, 1);
}