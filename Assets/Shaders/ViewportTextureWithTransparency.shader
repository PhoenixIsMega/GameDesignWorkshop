#shader vertex
#version 330 core
uniform vec2 ViewportSize;
uniform vec2 CameraLocation;
uniform vec2 Scale;
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 aColor;
layout (location = 3) in float aIndex;
out vec2 texCoord;
out vec4 color;
out float texIndex;

void main() 
{
	float distanceFromCenterX = aPosition.x - (CameraLocation.x + 0.5 * ViewportSize.x);
	float scaledDistanceX = distanceFromCenterX * Scale.x;
	float normalizedScaledDistanceX = scaledDistanceX / (0.5 * ViewportSize.x);
	float nx = normalizedScaledDistanceX;

	float distanceFromCenterY = aPosition.y - (CameraLocation.y + 0.5 * ViewportSize.y);
	float scaledDistanceY = distanceFromCenterY * Scale.y;
	float normalizedScaledDistanceY = scaledDistanceY / (0.5 * ViewportSize.y);
	float ny = normalizedScaledDistanceY;

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