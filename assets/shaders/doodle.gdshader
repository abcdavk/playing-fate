shader_type canvas_item;
render_mode world_vertex_coords;

group_uniforms Squiggle;
/**
 * Noise texture scale
 * By default, the noise texture's size in world coordinates is set by its resolution
*/
uniform vec2 scale = vec2(1.0);
uniform float strength = 1.0;
/**
 * Number of squiggle frames per second
*/
uniform float fps = 6.0;
uniform sampler2D noise : filter_linear, repeat_enable;
group_uniforms Jigle;

varying vec4 modulate;
varying vec2 noise_uv;

uniform float amplitude : hint_range(0.0, 50.0) = 10.0; // The height of the bobbing in pixels
uniform float speed : hint_range(0.0, 10.0) = 2.0;     // The speed of the bobbing
uniform bool horizontal_anim = true;
uniform bool vertical_anim = true;

float random (vec2 uv) {
    return fract(sin(dot(uv.xy,
        vec2(12.9898,78.233))) * 43758.5453123);
}

varying float seed;

void vertex() {
	modulate = COLOR;
	noise_uv = (VERTEX - MODEL_MATRIX[3].xy) / (vec2(textureSize(noise, 0)) * scale);

	seed = random(MODEL_MATRIX[3].xy);
}

// Use irrational constants for unique squiggles every frame
#define offset_multiplier vec2(PI, E)

void fragment() {
	float t = TIME * speed + seed * TAU * 10.0;
	float offsetY;
	float offsetX;
	if (vertical_anim) {
		offsetY = amplitude * sin(t);
	} else {
		offsetY = 0.0;
	}
	if (horizontal_anim) {
		offsetX = amplitude * cos(t);
	} else {
		offsetX = 0.0;
	}
	vec2 noise_offset = vec2(floor(t * fps)) * offset_multiplier;
	float noise_sample = texture(noise, noise_uv + noise_offset).r * 4.0 * PI;
	vec2 direction = vec2(cos(noise_sample), sin(noise_sample));
	vec2 squiggle_uv = UV + direction * strength * 0.005;
	
	COLOR = texture(TEXTURE, squiggle_uv + vec2(offsetX, offsetY) / 500.0) * modulate;
}