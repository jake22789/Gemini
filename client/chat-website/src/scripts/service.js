export async function sendMessageToAI(message) {
    const response = await fetch('http://localhost:5298/ai'+`?prompt=${encodeURIComponent(message)}`);

    if (!response.ok) {
        const message = await response.text();
        console.log('Error:', message,response);
        throw new Error(message);
    }

    const data = await response.text();
    return data;
}
