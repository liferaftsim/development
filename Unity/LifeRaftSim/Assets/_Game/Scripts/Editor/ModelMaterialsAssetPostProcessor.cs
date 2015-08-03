using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Checks if there is already a material that does the same and uses that instead.
    /// This is good for models exported to FBX using Lightwave since Lightwave creates a unique material per submesh even though the submeshes uses the same material.
    /// </summary>
    /// <remarks>
    /// Support for shaders with multiple textures not yet implemented.
    /// </remarks>
    public class ModelMaterialsAssetPostProcessor : AssetPostprocessor
    {
        private Material OnAssignMaterialModel(Material sourceMaterial, Renderer renderer)
        {
            var modelRelativeFileName = this.assetPath;
            var modelFolder = Path.GetDirectoryName(modelRelativeFileName);
            var materialsFolder = Path.Combine(modelFolder, "Materials");
            var modelFileName = Path.GetFileNameWithoutExtension(this.assetPath);
            var lwModelFullFileName = Path.Combine(modelFolder, modelFileName + ".lwo");

            // only apply this processor to models exported using Lightwave
            if (!File.Exists(lwModelFullFileName))
            {
                return null;
            }

            var materialIds = AssetDatabase.FindAssets("t:Material", new[] { materialsFolder });
            foreach (var materialId in materialIds)
            {
                var materialPath = AssetDatabase.GUIDToAssetPath(materialId);
                var material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
                if (material == null)
                {
                    Debug.LogError("Material not loaded");
                    continue;
                }

                var doesTheSame
                    = sourceMaterial.color == material.color
                    && sourceMaterial.globalIlluminationFlags == material.globalIlluminationFlags
                    && sourceMaterial.mainTexture == material.mainTexture
                    && sourceMaterial.mainTextureOffset == material.mainTextureOffset
                    && sourceMaterial.mainTextureScale == material.mainTextureScale
                    && sourceMaterial.passCount == material.passCount
                    && sourceMaterial.renderQueue == material.renderQueue
                    && sourceMaterial.shader == material.shader
                    && sourceMaterial.shaderKeywords.All(k1 => material.shaderKeywords.Any(k2 => k1 == k2))
                    ;

                if (doesTheSame)
                {
                    return material;
                }
            }

            return null;
        }
    }
}
