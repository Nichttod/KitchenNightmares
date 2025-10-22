using UnityEngine;

public class CuttingCounerVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCouner_OnCut;
        
    }
      private void CuttingCouner_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
    
}
