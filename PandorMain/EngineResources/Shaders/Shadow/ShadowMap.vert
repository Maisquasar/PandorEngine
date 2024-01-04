#version 330 core
layout (location = 0) in vec3 aPos;

uniform mat4 MVP;

out vec2 texCoord;
out vec3 normal;
out vec3 fragPos;
out vec3 tangent;
out vec4 color;
out vec4 fragPosLight;

void main()
{
    gl_Position = vec4(aPos, 1.0) * MVP;
}