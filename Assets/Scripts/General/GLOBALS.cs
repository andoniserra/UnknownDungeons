using UnityEngine;
using System.Collections;

public class GLOBALS : MonoBehaviour
{
	public static int PIXELS_TO_UNITS = 8	;
	public static float UNITS_TO_PIXELS = 1f / PIXELS_TO_UNITS	;

	public enum Direction { North, East, South, West };

	public enum Action { Wait, Jump, Fly };
}
