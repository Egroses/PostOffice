using UnityEngine;

namespace Codes.Char
{
    public class CharAnimation : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void RunningOn()
        {
            _animator.SetBool("running",true);
        }
        public void RunningOff()
        {
            _animator.SetBool("running",false);
        }

        public void CarryingOn()
        {
            _animator.SetBool("carrying",true);
        }
        public void CarryingOff()
        {
            _animator.SetBool("carrying",false);
        }
    }
}
