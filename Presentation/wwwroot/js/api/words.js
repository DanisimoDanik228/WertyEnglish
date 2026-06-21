async function GetAllWords() {
    let response = await fetch('/api/Word/GetAllWords');

    return await response.json();
}

async function TranslateWord(word) {
    let response = await fetch(`/api/Word/TranslateWord?Word=${word}`);

    return await response.text();
}



async function CreatePairWord(word, translate) {
    let response = await fetch(`/api/Word/CreatePairWord?Word=${word}&Translate=${translate}`,
        {
            method:"POST"
        });

    return await response.json();
}

async function UpdatePairWord(word) {
    let response = await fetch(`/api/Word/UpdatePairWord`,
        {
            method: "PATCH",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(word)
        });

    return await response.json();
}