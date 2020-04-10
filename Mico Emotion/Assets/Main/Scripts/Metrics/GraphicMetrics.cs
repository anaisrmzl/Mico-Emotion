using UnityEngine;

namespace Emotion.Metrics
{
    public class GraphicMetrics : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private RandomImage[] randomImages;
        [SerializeField] private RectTransform[] points;
        [SerializeField] private RectTransform[] yPoints;
        [SerializeField] private LineRenderer[] lineRenderers;

        #endregion

        #region BEHAVIORS

        private void Start()
        {
            BuildGraph();
        }

        private void BuildGraph()
        {
            for (int i = 0; i < points.Length; i++)
                points[i].transform.position = new Vector3(randomImages[i].transform.position.x, yPoints[randomImages[i].Index].transform.position.y, 0.0f);

            for (int i = 0; i < lineRenderers.Length; i++)
                for (int j = 0; j < lineRenderers[i].positionCount; j++)
                    lineRenderers[i].SetPosition(j, new Vector3(points[i + j].anchoredPosition.x, points[i + j].anchoredPosition.y, 0.0f));
        }

        #endregion
    }
}
