#version 330
 
// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
in vec3 ambientc;				// ambient light color
in vec4 worldPos;
uniform sampler2D pixels;		// texture sampler
uniform vec3 lightPos;          // light position
uniform vec4 cPos;
// shader output
out vec4 outputColor;

// fragment shader
void main()
{
	vec3 L = lightPos - worldPos.xyz;
	float dist = L.length();
	float specularStrength = 0.5;
	vec3 K = normalize( L );
	vec3 viewDir = normalize(cPos.xyz - worldPos.xyz);
	vec3 reflectDir = reflect(-L, normal.xyz);
	vec3 lightColor = vec3( 5, 5, 5 );
	float ambientStrength = 0.01;
    vec3 ambient = ambientStrength * lightColor;
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
	vec3 materialColor = texture( pixels, uv ).xyz;
	vec3 specular = specularStrength * spec * materialColor; 
	float attenuation = 1.0f / (dist *dist);
	outputColor = vec4( ((materialColor * max( 0.0f, dot( K, normal.xyz ) ) *
	attenuation) + specular + ambient)*lightColor, 1 );
}