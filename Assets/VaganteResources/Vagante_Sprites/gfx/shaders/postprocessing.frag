uniform sampler2D texture;
uniform float gamma;
void main()
{
	vec4 pixel = texture2D(texture, gl_TexCoord[0].xy);
	vec3 rgbComponent = pixel.rgb;
	float invGamma = 1.0/gamma;
	rgbComponent.r = pow(rgbComponent.r, invGamma);
	rgbComponent.g = pow(rgbComponent.g, invGamma);
	rgbComponent.b = pow(rgbComponent.b, invGamma);
	vec4 corrected = vec4(rgbComponent, pixel.a);
	gl_FragColor = gl_Color * corrected;
}
