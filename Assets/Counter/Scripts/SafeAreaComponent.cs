using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaComponent : MonoBehaviour
{
	private RectTransform rectTransform;

	// Start is called before the first frame update
	void Start()
	{
		rectTransform = this.GetComponent<RectTransform>();
		ApplySafeArea();
	}

	private void ApplySafeArea()
	{
		Rect safeAreaRect = Screen.safeArea;

		float scaleRatio = rectTransform.rect.width / Screen.width;

		var left = safeAreaRect.xMin * scaleRatio;
		var right = -( Screen.width - safeAreaRect.xMax ) * scaleRatio;
		var top = -safeAreaRect.yMin * scaleRatio;
		var bottom = ( Screen.height - safeAreaRect.yMax ) * scaleRatio;

		rectTransform.offsetMin = new Vector2( left, bottom );
		rectTransform.offsetMax = new Vector2( right, top);
	}
}
