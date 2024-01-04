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
uniform vec3 CamUp;
uniform vec3 CamRight;
uniform float BillboardSize = 0.5f;

void main()
{
	vec3 wPos = CamUp * aPos.y * BillboardSize - CamRight * aPos.x * BillboardSize;
    gl_Position = vec4(wPos, 1.0f) * MVP;

	normal = aNor;
	texCoord = aTex;
	color = vec4(1.0);

}