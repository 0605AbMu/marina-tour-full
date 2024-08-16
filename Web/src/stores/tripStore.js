import { defineStore } from 'pinia'
import { ref } from 'vue'
export const useTripStore = defineStore('trip-store', () => {
  const trips = ref([
    {
      img: '/images/slides/1.jpg',
      price: 10080000,
      title: 'Bali',
      region: 'Indonesia',
      star: 4
    },
    {
      img: '/images/slides/2.jpg',
      price: 1134000,
      title: 'Antalya',
      region: 'Turkey',
      star: 4.5
    },
    {
      img: '/images/slides/3.jpg',
      price: 8190000,
      title: 'Istanbul',
      region: 'Turkey',
      star: 4
    },
    {
      img: '/images/slides/4.jpg',
      price: 9450000,
      title: 'Thailand',
      region: 'Thailand',
      star: 4
    }
  ])

  const formatPrice = (amount) => {
    return (
      new Intl.NumberFormat('uz-UZ', {
        style: 'currency',
        currency: 'UZS',
        maximumFractionDigits: 20,
        notation: 'standard'
      })
        .formatToParts(amount)
        .map((x) => {
          if (x.type === 'group') return ' '
          else if (x.type == 'currency') return ''
          else return x.value
        })
        .join('') + ' UZS'
    )
  }

  return {
    trips,
    formatPrice
  }
})

export default useTripStore
