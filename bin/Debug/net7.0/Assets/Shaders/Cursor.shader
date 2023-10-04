#shader vertex
#version 330 core
uniform vec2 ViewportSize;
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in float aIndex;
out vec2 texCoord;
out float texIndex;

void main() 
{
	float nx = aPosition.x / ViewportSize.x * 2f - 1f;
	float ny = aPosition.y / ViewportSize.y * 2f - 1f;
	texIndex = aIndex;
	texCoord = aTexCoord;
	gl_Position = vec4(nx, ny, 0.0f, 1.0);
}

#shader fragment
#version 330 core
out vec4 outputColor;
in vec2 texCoord;
in float texIndex;
uniform sampler2D u_Texture[2];

void main() 
{
	int index = int(texIndex);
	vec4 texColor = texture(u_Texture[index], texCoord);
	if(texColor.a < 0.1)
        discard;
	outputColor = texColor;
}