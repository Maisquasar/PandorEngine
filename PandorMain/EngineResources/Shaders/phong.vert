#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTex;
layout (location = 2) in vec3 aNor;
layout (location = 3) in vec3 aTan;

out vec2 texCoord;
out vec3 normal;
out vec3 fragPos;
out vec3 tangent;
out vec4 color;
out vec4 fragPosLight;

uniform mat4 MVP;
uniform mat4 model;
uniform mat4 lightSpaceMatrix;

void main()
{
	gl_Position = vec4(aPos, 1.0) * MVP;
	texCoord = aTex;
	
	tangent = (model * vec4(normalize(aTan), 0)).xyz;
	normal = vec3(vec4(aNor, 0.0) * model);
	fragPos = vec3(vec4(aPos, 1.0) * model);
	color = vec4(1.0);
	fragPosLight =  vec4(fragPos.x, fragPos.y, -fragPos.z, 1.0) * lightSpaceMatrix;
}

