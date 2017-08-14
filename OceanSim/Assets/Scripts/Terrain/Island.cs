using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island {


    private Vector2 position;
    private float radius;
    private MyNoise noise;
    //private int seed;
    //private float noiseScale;

    //private Vector2 offset;

    public Island(Vector2 _position, float _radius, int _seed, float _noiseScale) {
        position = _position;
        radius = _radius;
        noise = new MyNoise(_seed, _noiseScale, 4, 0.5f, 2f);
    }

    public Island(Vector2 _position, float _radius, MyNoise _noise) {
        noise = _noise;
        position = _position;
        radius = _radius;
    }

    public float getHeight(Vector2 _position) {
        float distToCenter = Vector2.Distance(_position, position);
        if (distToCenter > radius) return 0.0f;
        float noiseHeight = noise.getHeight(_position);
        float fadeT = 1 - distToCenter / radius;
        return noiseHeight * fadeT;
    }

    public float[,] getHeightMap(int xRes, int yRes, float width, float height, Vector2 botLeftPos) {
        float[,] heightMap = new float[xRes, yRes];
        float xStepSize = width / xRes;
        float yStepSize = height / yRes;
        for(int y = 0; y < yRes; y++) {
            for(int x = 0; x < xRes; x++) {
                heightMap[x, y] = getHeight(botLeftPos + new Vector2(x * xStepSize, y * yStepSize));
            } 
        }

        return heightMap;
    }


}
