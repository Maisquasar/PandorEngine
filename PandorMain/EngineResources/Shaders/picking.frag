#version 330 core

out vec4 outColor;

in vec2 texCoord;
in vec3 normal;
in vec3 fragPos;
in vec3 tangent;
in vec4 color;
in vec4 fragPosLight;

uniform vec4 PickingColor;

void main()
{
    outColor = PickingColor;
}