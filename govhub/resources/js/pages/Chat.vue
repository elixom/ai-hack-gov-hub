<template>
  <div class="flex flex-col h-full">
    <!-- Header -->
    <div class="relative px-6 py-4 overflow-hidden shadow-lg bg-gradient-to-r from-green-600 via-yellow-400 to-green-600">
      <div class="absolute inset-0 bg-black opacity-10"></div>
      <div class="relative z-10 flex items-center gap-3">
        <div class="flex items-center justify-center w-12 h-12 border-2 border-white rounded-full shadow-lg bg-gradient-to-br from-yellow-400 to-green-600">
          <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="text-white">
            <path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2z"></path>
          </svg>
        </div>
        <div>
          <h1 class="text-2xl font-bold text-white drop-shadow-md">GovHub</h1>
          <p class="text-sm font-medium text-yellow-100">One Love, One Chat 🇯🇲</p>
        </div>
      </div>
    </div>

    <!-- Messages Container -->
    <div
      ref="chatContainer"
      class="flex-1 px-4 py-6 space-y-4 overflow-y-auto"
    >
      <div
        v-for="message in messages"
        :key="message.id"
        class="flex"
        :class="message.role === 'user' ? 'justify-end' : 'justify-start'"
      >
        <div
          class="max-w-2xl"
          :class="message.role === 'user' ? 'order-2' : 'order-1'"
        >
          <div
            class="px-4 py-3 shadow-md rounded-2xl"
            :class="message.role === 'user'
              ? 'bg-gradient-to-br from-yellow-400 to-yellow-500 text-gray-900 rounded-br-none'
              : 'bg-gradient-to-br from-green-600 to-green-700 text-white rounded-bl-none '"
          >
            <p class="text-sm font-medium leading-relaxed whitespace-pre-wrap">{{ message.content }}</p>
          </div>
          <p
            class="px-1 mt-1 text-xs font-medium text-gray-600"
            :class="message.role === 'user' ? 'text-right' : 'text-left'"
          >
            {{ formatTime(message.timestamp) }}
          </p>
        </div>
      </div>

      <!-- Loading Indicator -->
      <div v-if="isLoading" class="flex justify-start">
        <div class="px-4 py-3 text-white border-2 border-green-800 rounded-bl-none shadow-md bg-gradient-to-br from-green-600 to-green-700 rounded-2xl">
          <div class="flex items-center gap-2">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="text-yellow-300 animate-spin">
              <path d="M21 12a9 9 0 1 1-6.219-8.56"></path>
            </svg>
            <span class="text-sm font-medium">Thinking irie thoughts...</span>
          </div>
        </div>
      </div>
    </div>

    <!-- Input Area -->
    <div class="px-4 py-4 bg-gray-600 shadow-lg">
      <div class="max-w-4xl mx-auto">
        <div class="flex items-center gap-2">
          <div class="relative flex-1">
            <textarea
              v-model="userInput"
              @keydown="handleKeydown"
              placeholder="Type yuh message..."
              rows="1"
              class="w-full px-4 py-3 pr-12 font-medium text-gray-900 placeholder-gray-500 bg-white border-2 border-green-700 shadow-inner resize-none rounded-2xl focus:outline-none focus:ring-2 focus:ring-yellow-400 focus:border-yellow-400"
              :disabled="isLoading"
            ></textarea>
          </div>
          <button
            @click="sendMessage"
            :disabled="!userInput.trim() || isLoading"
            class="flex items-center justify-center flex-shrink-0 w-12 h-12 text-gray-900 transition-all transform border-2 border-yellow-600 rounded-full shadow-lg bg-gradient-to-br from-yellow-400 to-yellow-500 hover:from-yellow-500 hover:to-yellow-600 disabled:opacity-50 disabled:cursor-not-allowed hover:scale-105"
          >
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
              <path d="m22 2-7 20-4-9-9-4Z"></path>
              <path d="M22 2 11 13"></path>
            </svg>
          </button>
        </div>
        <p class="mt-2 text-xs font-medium text-center text-white drop-shadow">
          Press Enter fi send, Shift+Enter fi new line
        </p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      messages: [
        {
          id: 1,
          role: 'assistant',
          content: 'Wah gwaan! How mi can help yuh today?',
          timestamp: new Date()
        }
      ],
      userInput: '',
      isLoading: false,
      chatContainer: null
    };
  },
  mounted() {
    this.chatContainer = this.$refs.chatContainer;
  },
  methods: {
    async sendMessage() {
      if (!this.userInput.trim() || this.isLoading) return;

      const userMessage = {
        id: Date.now(),
        role: 'user',
        content: this.userInput.trim(),
        timestamp: new Date()
      };

      this.messages.push(userMessage);
      this.userInput = '';
      this.isLoading = true;

      this.$nextTick(() => {
        this.scrollToBottom();
      });

      // Simulate AI response - replace with actual API call
      setTimeout(() => {
        const aiMessage = {
          id: Date.now() + 1,
          role: 'assistant',
          content: this.generateResponse(userMessage.content),
          timestamp: new Date()
        };

        this.messages.push(aiMessage);
        this.isLoading = false;

        this.$nextTick(() => {
          this.scrollToBottom();
        });
      }, 1000 + Math.random() * 1000);

      // Replace above with actual Laravel API call:
      // try {
      //   const response = await axios.post('/api/chat', {
      //     message: userMessage.content
      //   });
      //
      //   const aiMessage = {
      //     id: Date.now() + 1,
      //     role: 'assistant',
      //     content: response.data.message,
      //     timestamp: new Date()
      //   };
      //   this.messages.push(aiMessage);
      // } catch (error) {
      //   console.error('Error:', error);
      // } finally {
      //   this.isLoading = false;
      // }
    },
    generateResponse(userInput) {
      const responses = [
        "Dat's a irie question, mi friend! Let mi help yuh wid dat.",
        "Mi understand wah yuh asking. Here's mi perspective pon dat...",
        "Respect! Mi happy fi elaborate pon dat topic.",
        "Based pon wah yuh share, mi tink di best approach would be...",
        "Let mi break dat down fi yuh inna clear way, seen?"
      ];
      return responses[Math.floor(Math.random() * responses.length)];
    },
    scrollToBottom() {
      if (this.chatContainer) {
        this.chatContainer.scrollTop = this.chatContainer.scrollHeight;
      }
    },
    formatTime(date) {
      return new Intl.DateTimeFormat('en-US', {
        hour: 'numeric',
        minute: '2-digit',
        hour12: true
      }).format(date);
    },
    handleKeydown(e) {
      if (e.key === 'Enter' && !e.shiftKey) {
        e.preventDefault();
        this.sendMessage();
      }
    }
  }
};
</script>

<style scoped>
/* Custom Jamaica-themed styles */
</style>
