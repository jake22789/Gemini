import{ sendMessageToAI } from "./service.js";

const chatForm = document.getElementById('chat-form');
const chatInput = document.getElementById('chat-input');
const chatMessages = document.getElementById('chat-messages');

chatForm.addEventListener('submit', async (event) => {
    event.preventDefault();
    const spinner = document.getElementById('spinner');
    spinner.classList.remove('hidden');

    const userMessage = chatInput.value;
    appendMessage('You', userMessage);
    
    try {
        const aiReply = await sendMessageToAI(userMessage);
        appendMessage('AI', aiReply);
    } catch (error) {
        appendMessage('Error', Error.message);
        console.error('Error:', error);
    }
    chatInput.value = '';
    spinner.classList.add('hidden');
});

function appendMessage(sender, message) {
    const messageElement = document.createElement('div');
    messageElement.classList.add('message');
    messageElement.innerHTML = `<strong>${sender}:</strong> ${marked.parse(message)}<script> alert('hi');</script>`;
    chatMessages.appendChild(messageElement);
    chatMessages.scrollTop = chatMessages.scrollHeight;
}