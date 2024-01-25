#shader vertex
#version 330 core

layout (location = 0) in vec2 aPosition;
layout (location = 1) in vec2 aTexCoord;

out vec2 texCoord;

void main() 
{
	texCoord = aTexCoord;
	gl_Position = vec4(aPosition.x, aPosition.y, 0.0, 1.0);
}

#shader fragment
#version 330 core
out vec4 outputColor;
in vec2 texCoord;

uniform sampler2D screenTexture;

void main() 
{
    //vec3 texColor = texture(screenTexture, texCoord).rgb;
	//if(texColor.a < 0.1)
    //    discard;
    
    outputColor = texture(screenTexture, texCoord);
    float average = 0.2126 * outputColor.r + 0.7152 * outputColor.g + 0.0722 * outputColor.b;
    outputColor = vec4(average, average, average, 1.0);
    
	//outputColor = vec4(texColor, 1.0);
}