#version 330 core
layout (location = 0) in vec3 aPos;

out vec2 texCoord;
out vec3 normal;
out vec3 fragPos;
out vec3 tangent;
out vec4 color;
out vec4 fragPosLight;

uniform mat4 VP;

void main()
{
   fragPos = aPos;
   vec4 pos = vec4(aPos, 1.0) * VP;
   gl_Position = pos.xyww;
}