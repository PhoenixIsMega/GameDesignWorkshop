#shader vertex
#version 330 core
uniform vec2 ViewportSize;
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 aColor;
layout (location = 3) in float aIndex;
out vec2 texCoord;
out vec4 color;
out float texIndex;

void main() 
{
	float nx = aPosition.x / ViewportSize.x * 2f - 1f;
	float ny = aPosition.y / ViewportSize.y * 2f - 1f;
	color = vec4(aColor.rgb, 1.0);
	texIndex = aIndex;
	texCoord = aTexCoord;
	gl_Position = vec4(nx, ny, 0.0f, 1.0);
}

#shader fragment
#version 330 core
out vec4 outputColor;
in vec2 texCoord;
in vec4 color;
in float texIndex;
uniform sampler2D u_Texture[2];
uniform float blackness;

void main() 
{
	int index = int(texIndex);
	vec4 texColor = texture(u_Texture[index], texCoord);
	if(texColor.a < 0.1)
        discard;
	outputColor = texColor * color * blackness;
}