#shader vertex
#version 330 core
uniform vec2 ViewportSize;
uniform vec2 CameraLocation;
uniform vec2 Scale;
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec4 aColor;

out vec4 color;

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

	color = vec4(aColor);
	gl_Position = vec4(nx, ny, 0.0f, 1.0);
}

#shader fragment
#version 330 core
out vec4 outputColor;
in vec4 color;

void main() 
{
	outputColor = color;
}