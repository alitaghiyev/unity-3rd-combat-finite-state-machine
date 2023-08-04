using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    public void ChangeScene(int index){//reset button
        SceneManager.LoadScene(index);
    }
}
