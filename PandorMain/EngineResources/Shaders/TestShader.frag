#version 450 core
out vec4 FragColor;

in vec2 texCoord;
in vec3 normal;
in vec3 fragPos;
in vec3 tangent;
in vec4 color;
in vec4 fragPosLight;

uniform sampler2D tex0;
uniform vec3 testV;

void main()
{
	FragColor = texture(tex0, texCoord);
	FragColor *= vec4(testV ,1);
}