async function GetAllWords(dictionaryId) {
    let response = await fetch(`/api/Word/GetAllWords?DitionaryId=${dictionaryId}`);

    return await response.json();
}

async function TranslateWord(word) {
    let response = await fetch(`/api/Word/TranslateWord?Word=${word}`);

    return await response.text();
}



async function CreatePairWord(dictionaryId, word, translate) {
    let response = await fetch(`/api/Word/CreatePairWord?DitionaryId=${dictionaryId}&Word=${word}&Translate=${translate}`,
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

async function DeletePairWord(dictionaryId, id) {
    let response = await fetch(`/api/Word/DeletePairWord?DitionaryId=${dictionaryId}&Id=${id}`,
        {
            method: "DELETE"
        });

    return await response.json();
}