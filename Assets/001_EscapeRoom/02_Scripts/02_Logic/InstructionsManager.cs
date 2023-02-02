using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour
{
  public List<GameObject> InstructionPages;
  public Button LeftArrow;
  public Button RightArrow;

  public void ShowPages()
  {
    InstructionPages.ForEach(x => x.SetActive(false));
    InstructionPages[0].SetActive(true);
    LeftArrow.gameObject.SetActive(false);
    RightArrow.gameObject.SetActive(true);
  }

  public void RightArrowClick()
  {
    ActivateArrowButtons();

    var nextActiveIndex = 0;
    for (var i = 0; i < InstructionPages.Count; i++)
    {
      if (InstructionPages[i].activeSelf)
      {
        nextActiveIndex = i + 1;
        break;
      }
    }

    InstructionPages.ForEach(x => x.SetActive(false));
    InstructionPages[nextActiveIndex].SetActive(true);
    if (nextActiveIndex + 1 == InstructionPages.Count)
      RightArrow.gameObject.SetActive(false);
  }

  public void LeftArrowClick()
  {
    ActivateArrowButtons();

    var nextActiveIndex = 0;
    for (var i = 0; i < InstructionPages.Count; i++)
    {
      if (InstructionPages[i].activeSelf)
      {
        nextActiveIndex = i - 1;
        break;
      }
    }

    InstructionPages.ForEach(x => x.SetActive(false));
    InstructionPages[nextActiveIndex].SetActive(true);
    if (nextActiveIndex == 0)
      LeftArrow.gameObject.SetActive(false);
  }

  private void ActivateArrowButtons()
  {
    LeftArrow.gameObject.SetActive(true);
    RightArrow.gameObject.SetActive(true);
  }
}
