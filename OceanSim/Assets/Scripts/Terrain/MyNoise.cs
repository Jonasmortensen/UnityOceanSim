using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNoise {

    private Vector2[] _octaveOffsets;
    private float _scale;
    private float _persistance;
    private float _lacunarity;

    public MyNoise(int seed, float scale, int octaves, float persistance, float lacunarity) {
        System.Random prng = new System.Random(seed);

        _octaveOffsets = new Vector2[octaves];
        for(int i = 0; i < octaves; i++) {
            float xOffset = prng.Next(-100000, 100000);
            float yOffset = prng.Next(-100000, 100000);
            _octaveOffsets[i] = new Vector2(xOffset, yOffset);
        }

        _scale = scale;
        _persistance = persistance;
        _lacunarity = lacunarity;
    }

    public float getHeight(Vector2 position) {
        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;

        for(int i = 0; i < _octaveOffsets.Length; i++) {
            float sampleX = position.x / _scale * frequency + _octaveOffsets[i].x;
            float sampleY = position.y / _scale * frequency + _octaveOffsets[i].y;
            float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);

            noiseHeight += perlinValue * amplitude;

            amplitude *= _persistance;
            frequency *= _lacunarity;
        }

        return noiseHeight;
    }

}
