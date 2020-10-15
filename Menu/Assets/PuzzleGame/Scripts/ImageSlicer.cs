using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
public static class ImageSlicer {
    public static Texture2D[, ] GetSlices (Texture2D image, int blocksPerLine) {
        int imageSize = Mathf.Min (image.width, image.height);
        int blockSize = imageSize / blocksPerLine;

        Texture2D[, ] blocks = new Texture2D[blocksPerLine, blocksPerLine];

        for (int x = 0; x < blocksPerLine; x++) {
            for (int y = 0; y < blocksPerLine; y++) {
                Texture2D puzzleBlock = new Texture2D (blockSize, blockSize);
                puzzleBlock.SetPixels (image.GetPixels (x * blockSize, y * blockSize, blockSize, blockSize));
                puzzleBlock.Apply ();
                puzzleBlock = addBorder(puzzleBlock);
                blocks[x, y] = puzzleBlock;
            }
        }
        return blocks;
    }

    public static Texture2D addBorder (Texture2D image) {
        for (int i = 0; i < image.width; i++) {
            image.SetPixel (i, 0, UnityEngine.Color.white);
            image.SetPixel (i, image.height - 1, UnityEngine.Color.white);
        }

        for (int j = 0; j < image.height; j++) {
            image.SetPixel (0, j, UnityEngine.Color.white);
            image.SetPixel (image.width - 1, j, UnityEngine.Color.white); 
        }
        image.Apply ();
        return image;
    }
}