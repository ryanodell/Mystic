#shader vertex
#version 330 core
layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;
layout(location = 2) in vec3 aColor;
out vec2 texCoord;
out vec4 color;

uniform mat4 u_ViewProjection;

void main(void)
{
    color = vec4(aColor, 1.0);
    texCoord = aTexCoord;
    gl_Position = u_ViewProjection * vec4(aPosition, 1.0);
}

#shader fragment
#version 330
out vec4 outputColor;
in vec2 texCoord;
in vec4 color;
uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, texCoord) * color;
}