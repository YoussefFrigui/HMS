export function formatDate(dateString) {
  const options = { year: 'numeric', month: 'long', day: 'numeric' }
  return new Date(dateString).toLocaleDateString(undefined, options)
}

export function handleError(error) {
  // You can enhance this to handle different error types
  console.error(error)
  alert(error.message || 'An unexpected error occurred')
}