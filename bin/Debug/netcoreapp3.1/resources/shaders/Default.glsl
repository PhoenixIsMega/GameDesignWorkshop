#shader vertex
#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aColor;
out vec4 vertexColor;

void main() 
{
	vertexColor = vec4(aColor.rgb, 1.0);
	gl_Position = vec4(aPosition.xyz, 1.0);
}

#shader fragment
#version 330 core
in vec4 vertexColor;
out vec4 color;

void main() 
{
	color = vertexColor;
}