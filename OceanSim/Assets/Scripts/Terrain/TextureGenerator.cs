using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator {
    public static Texture2D TextureFromColormap(Color[] colorMap, int width, int height) {
        Texture2D texture = new Texture2D(width, height);
        //texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromColormap(Color[] colorMap, int width, int height, Texture2D baseTexture, float baseTextureScale) {
        Texture2D texture = new Texture2D(width, height);
        //texture.filterMode = FilterMode.Point;
        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                colorMap[y * width + x] *= baseTexture.GetPixelBilinear((x / baseTextureScale) % 1.0f, (y / baseTextureScale) % 1.0f);
            }
        }
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap) {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                colorMap[y * width + x] = Color.Lerp(Color.white, Color.black, heightMap[x, y]);
            }
        }

        return TextureFromColormap(colorMap, width, height);
    }
}
