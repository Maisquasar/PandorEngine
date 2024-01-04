#version 330 core

in vec2 texCoord;
in vec3 normal;
in vec3 fragPos;
in vec3 tangent;
in vec4 color;
in vec4 fragPosLight;

void main()
{     
	gl_FragDepth = gl_FragCoord.z;
}  