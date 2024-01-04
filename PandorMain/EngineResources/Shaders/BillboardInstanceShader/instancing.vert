#version 330 core
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTex;
layout(location = 2) in vec3 aNor;
layout(location = 8) in vec4 xyzs;
layout(location = 9) in vec4 aCol;

uniform vec3 CamUp;
uniform vec3 CamRight;
uniform vec3 CamForward;
uniform mat4 VPMatrix;

out vec2 texCoord;
out vec3 normal;
out vec3 fragPos;
out vec3 tangent;
out vec4 color;
out vec4 fragPosLight;

void main()
{
	float size =  xyzs.w; // because we encoded it this way.
	vec3 position = xyzs.xyz;
	vec3 wPos = position - CamForward * aPos.z * size + CamUp * aPos.y * size - CamRight * aPos.x * size;
	gl_Position = vec4(wPos, 1.0) * VPMatrix;
	texCoord = aTex;
	normal = aNor;
	color = aCol;
}


