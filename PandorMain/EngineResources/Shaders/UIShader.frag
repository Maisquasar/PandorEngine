#version 450 core
out vec4 FragColor;

in vec2 texCoord;
in vec3 normal;
in vec3 fragPos;
in vec3 tangent;
in vec4 color;
in vec4 fragPosLight;

uniform sampler2D tex0;
uniform bool enableTexture = false;
uniform vec4 ourColor = vec4(0, 0, 0, 0);

void main()
{
    vec4 finalColor;
    if (enableTexture) {
        finalColor = texture2D(tex0, texCoord) * ourColor;
    } else {
        finalColor = ourColor;
    }
    if (finalColor.a == 0)
        discard;
    else
        FragColor = finalColor;

}