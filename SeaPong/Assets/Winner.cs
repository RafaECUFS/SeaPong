using UnityEngine;
using UnityEngine.UI;

public class PainelController : MonoBehaviour
{
    public RectTransform painel; // Arraste o Panel aqui no Inspector

    public void MoverPainel(bool win)
    {
        Vector2 novaPosicao;
        if (win==true)
            novaPosicao = new Vector2(-430, 100);
        else
            novaPosicao = new Vector2(430, 100);

        painel.anchoredPosition = novaPosicao;
    }
    




}
