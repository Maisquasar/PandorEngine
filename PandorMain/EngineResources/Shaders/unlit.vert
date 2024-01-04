#version 450 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTex;
layout (location = 2) in vec3 aNor;

out vec2 texCoord;
out vec3 normal;
out vec3 fragPos;
out vec3 tangent;
out vec4 color;
out vec4 fragPosLight;

uniform mat4 MVP;

void main()
{
	gl_Position = vec4(aPos, 1.0) * MVP;
	texCoord = aTex;
	normal = aNor;
	color = vec4(1.0);
}