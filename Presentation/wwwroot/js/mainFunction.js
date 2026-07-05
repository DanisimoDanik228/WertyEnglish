const baseUrl = document.getElementById('audio-script').getAttribute('data-url');

async function getWordAudio(word) {
    const response = await fetch(`${baseUrl}${word}`);

    if (!response.ok)
    {
        return "";
    }

    const data = await response.json();

    const audioUrl = data
        .flatMap(entry => entry.phonetics)
        .find(p => p.audio && p.audio !== "")?.audio;

    return audioUrl;
}

async function voiceWord(word) {
    const audioUrl = await getWordAudio(word);

    if (audioUrl) {
        const audio = new Audio(audioUrl);
        audio.play();
    }
    else
    {
        alert("Sorry, no audio available for this word.");
    }
}