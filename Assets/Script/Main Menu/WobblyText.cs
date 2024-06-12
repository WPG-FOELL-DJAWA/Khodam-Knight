using UnityEngine;
using TMPro;

public class WobblyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Update()
    {
        _text.ForceMeshUpdate();
        var textInfo = _text.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2 + orig.x * .01f) * 10, 0);
            }

            for (int j = 0; j < textInfo.meshInfo.Length; ++j)
            {
                var meshInfo = textInfo.meshInfo[j];
                meshInfo.mesh.vertices = meshInfo.vertices;
                _text.UpdateGeometry(meshInfo.mesh, j);
            }
        }
    }
}
