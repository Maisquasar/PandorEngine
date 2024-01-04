#version 450 core
out vec4 FragColor;

in vec2 texCoord;
in vec3 normal;
in vec3 fragPos;
in vec3 tangent;
in vec4 color;
in vec4 fragPosLight;

uniform sampler2D albedo;
uniform bool enableTexture = false;
uniform vec4 ourColor = vec4(0, 0, 0, 0);

void main()
{
    if (enableTexture) {
        FragColor = texture2D(albedo, texCoord) * ourColor;
    } else {
        FragColor = ourColor;
    }
    if (FragColor.a <= 0.2f)
        discard;
}