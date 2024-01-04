#version 330 core
layout (location = 0) in vec3 aPos;

out vec2 texCoord;
out vec3 normal;
out vec3 fragPos;
out vec3 tangent;
out vec4 color;
out vec4 fragPosLight;

uniform mat4 MVP;
uniform float outlining;

void main()
{
    vec3 currentPos = vec3(outlining * vec4(aPos, 1.0f));
    gl_Position =  vec4(currentPos, 1.0f) * MVP;
}