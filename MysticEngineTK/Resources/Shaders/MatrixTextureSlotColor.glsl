#shader vertex
#version 330 core
layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;
layout(location = 2) in vec3 aColor;
layout(location = 3) in float aTexSlot;
out vec2 texCoord;
out vec4 color;
out float texSlot;
uniform mat4 u_model;
uniform mat4 u_Projection;

void main(void)
{
    texSlot = aTexSlot;
    color = vec4(aColor, 1.0);
    texCoord = aTexCoord;
    gl_Position = u_Projection * vec4(aPosition, 1.0);
}

#shader fragment
#version 330
out vec4 outputColor;
in vec2 texCoord;
in vec4 color;
in float texSlot;
uniform sampler2D u_Texture[2];

void main()
{
    int index = int(texSlot);
    outputColor = texture(u_Texture[index], texCoord) * color;
}