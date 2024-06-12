using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource _BGMSource;
    [SerializeField] private BGMSO _BGM;
    [Space]
    [Space]
    [SerializeField] private AudioSource _SFXSource1;
    [SerializeField] private AudioSource _SFXSource2;
    [SerializeField] private SoundSO _SFX;
    [Space]
    [Space]
    [SerializeField] private AudioSource _dubSource;
    [Space]
    [SerializeField] private float _delayToLoop;
    private float _delayToLoopTemp;
    private int _currentMusicIndex;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _delayToLoopTemp = _delayToLoop;
    }

    private void Update()
    {
        if (!_BGMSource.isPlaying) // noted : perlu di baca lebih lanjut
        {
            _delayToLoopTemp -= Time.deltaTime;
            if (_delayToLoopTemp <= 0 && _BGMSource.clip != null)
            {
                switchBGM(_currentMusicIndex);
            }
        }
    }

    public void playBGM(BGMName BGMName)
    {
        for (int i = 0; i < _BGM.ClipList.Count; i++)
        {
            if (_BGM.ClipList[i].name == BGMName)
            {
                if (_BGMSource.clip != _BGM.ClipList[i].clip)
                {
                    switchBGM(i);
                    break;
                }
            }
        }
    }

    public void stopBGM()
    {
        if (_BGMSource.isPlaying)
        {
            _BGMSource.Stop();
        }
        else
        {
            return;
        }
    }

    public void playSFX1(SoundName soundName)
    {
        for (int i = 0; i < _SFX.ClipList.Count; i++)
        {
            if (_SFX.ClipList[i].name == soundName)
            {
                if (_SFXSource1.clip != _SFX.ClipList[i].clip)
                {
                    _SFXSource1.volume = _SFX.ClipList[i].volume;
                    _SFXSource1.pitch = _SFX.ClipList[i].pitch;
                    _SFXSource1.clip = _SFX.ClipList[i].clip;
                    _SFXSource1.Play();
                    break;
                }
                else
                {
                    _SFXSource1.Play();
                    break;
                }
            }
        }
    }

    public void stopSFX1()
    {
        if (_SFXSource1.isPlaying)
        {
            _SFXSource1.Stop();
        }
        else
        {
            return;
        }
    }

    public void playSFX2(SoundName soundName)
    {
        for (int i = 0; i < _SFX.ClipList.Count; i++)
        {
            if (_SFX.ClipList[i].name == soundName)
            {
                if (_SFXSource2.clip != _SFX.ClipList[i].clip)
                {
                    _SFXSource2.volume = _SFX.ClipList[i].volume;
                    _SFXSource2.pitch = _SFX.ClipList[i].pitch;
                    _SFXSource2.clip = _SFX.ClipList[i].clip;
                    _SFXSource2.Play();
                    break;
                }
                else
                {
                    _SFXSource2.Play();
                    break;
                }
            }
        }
    }

    public void stopSFX2()
    {
        if (_SFXSource2.isPlaying)
        {
            _SFXSource2.Stop();
        }
        else
        {
            return;
        }
    }

    private void switchBGM(int musicIndex)
    {
        _currentMusicIndex = musicIndex;

        if (_BGMSource.isPlaying)
        {
            while (_BGMSource.volume > 0)
            {
                _BGMSource.volume -= 0.05f;
                // yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            _BGMSource.volume = 0;
        }

        _BGMSource.clip = _BGM.ClipList[musicIndex].clip;
        _BGMSource.pitch = _BGM.ClipList[musicIndex].pitch;
        _BGMSource.Play();

        _delayToLoopTemp = _delayToLoop;

        while (_BGMSource.volume < _BGM.ClipList[musicIndex].volume)
        {
            _BGMSource.volume += 0.05f;
            // yield return new WaitForSeconds(0.05f);
        }
    }



    public void playDub(AudioClip dubbing)
    {
        _dubSource.clip = dubbing;
        _dubSource.Play();
    }

    public void stopDub()
    {
        _dubSource.Stop();
    }
}
