#version 330
 
// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
in vec3 ambientc;				// ambient light color
in vec4 worldPos;
uniform sampler2D pixels;		// texture sampler
uniform vec3 lightPos;          // light position
uniform vec4 cPos;
uniform mat4 toMove;
// shader output
out vec4 outputColor;

// fragment shader
void main()
{
	vec4 cPost = inverse(toMove) * cPos;
	vec3 L = lightPos - worldPos.xyz;
	float dist = L.length();
	float specularStrength = 1;
	vec3 K = normalize( L );
	vec3 viewDir = normalize(cPost.xyz - worldPos.xyz);
	vec3 reflectDir = normalize(reflect(-L, normal.xyz));
	vec3 lightColor = vec3( 20, 20, 20 );
	float ambientStrength = 0.01;
    vec3 ambient = ambientStrength * lightColor;
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 128);
	vec3 materialColor = texture( pixels, uv ).xyz;
	vec3 specular = specularStrength * spec * materialColor * lightColor; 
	float attenuation = 1.0f / (dist * dist);
	float ndotl = max(0.0f, dot(K, normal.xyz));
	vec3 diffuse = materialColor * ndotl * lightColor * attenuation;
	outputColor = vec4(ambient, 1.0f) + vec4(diffuse + specular, 1.0f);
}