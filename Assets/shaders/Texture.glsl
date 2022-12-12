#shader vertex
#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
out vec2 texCoord;

void main() 
{
	texCoord = aTexCoord;
	gl_Position = vec4(aPosition.xyz, 1.0);
}

#shader fragment
#version 330 core
in vec2 texCoord;
out vec4 color;
uniform sampler2D texture0;

void main() 
{
	color = texture(texture0, texCoord);
}