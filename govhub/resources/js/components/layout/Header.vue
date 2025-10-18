<template>
  <header class="bg-white border-b border-gray-200 px-6 py-4">
    <div class="flex items-center justify-between">
      <div class="flex items-center space-x-4">
        <h1 class="text-xl font-semibold text-gray-900">{{ pageTitle }}</h1>
      </div>
      
      <div class="flex items-center space-x-4">
        <div class="flex items-center space-x-3">
          <div class="w-8 h-8 bg-blue-600 rounded-full flex items-center justify-center">
            <span class="text-white text-sm font-medium">{{ userInitials }}</span>
          </div>
          <div class="hidden sm:block">
            <p class="text-sm font-medium text-gray-900">{{ userName }}</p>
            <p class="text-xs text-gray-500">{{ userRole }}</p>
          </div>
        </div>
        
        <a
          href="/api/logout"
          class="text-gray-500 hover:text-gray-700 transition-colors duration-200"
          title="Sign Out"
        >
          <i class="fas fa-sign-out-alt"></i>
        </a>
      </div>
    </div>
  </header>
</template>

<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { useAuth } from '@/composables/useAuth'

const route = useRoute()
const { user } = useAuth()

const pageTitle = computed(() => {
  const routeName = route.name as string
  if (!routeName) return 'Dashboard'
  return routeName.charAt(0).toUpperCase() + routeName.slice(1)
})

const userName = computed(() => {
  if (!user.value) return 'User'
  const firstName = user.value.firstName || ''
  const lastName = user.value.lastName || ''
  return `${firstName} ${lastName}`.trim() || user.value.email || 'User'
})

const userInitials = computed(() => {
  if (!user.value) return 'U'
  const firstName = user.value.firstName || ''
  const lastName = user.value.lastName || ''
  return `${firstName[0] || ''}${lastName[0] || ''}`.toUpperCase() || 'U'
})

const userRole = computed(() => 'Healthcare Provider')
</script>