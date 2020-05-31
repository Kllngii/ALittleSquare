using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DisallowMultipleComponent]
[RequireComponent(typeof(TextMeshPro))]
public class I18nTMPro : MonoBehaviour {
  public string TextId;
  void Start() {
    var text = GetComponent<TextMeshPro>();
    if (text != null)
      if(TextId == "ISOCode")
        text.text = I18n.GetLanguage();
      else
        text.text = I18n.Fields[TextId];
  }
}
