using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    public void CloseBrifiengWindow() {

        if (this.gameObject.active) {

            this.gameObject.SetActive(false);

        }

    }
}
