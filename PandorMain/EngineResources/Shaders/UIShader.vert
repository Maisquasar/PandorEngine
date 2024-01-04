#version 450 core
layout (location = 0) in vec4 vertex; // <vec2 pos, vec2 tex>

out vec2 texCoord;
out vec3 normal;
out vec3 fragPos;
out vec3 tangent;
out vec4 color;
out vec4 fragPosLight;

uniform mat4 projection;
uniform float depth;

void main()
{
   gl_Position = projection * vec4(vertex.xy, depth, 1.0);
   texCoord = vertex.zw;
}