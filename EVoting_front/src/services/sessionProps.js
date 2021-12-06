export const COOKIE_EXPIRATION_DAYS = 18

const createCookie = name => [
  () => getCookie(name),
  (value) => {
    if (value != null)
      setCookie(name, value)
    else removeCookie(name)
  },
]

function setCookie(cookie_name, cookie_value) {
  const today = new Date()
  let expire = new Date()
  expire.setTime(today.getTime() + 60 * 60 * 1000 * 24 * COOKIE_EXPIRATION_DAYS)
  document.cookie = cookie_name + '=' + escape(cookie_value) + '; expires=' + expire.toUTCString()

}

function getCookie(cookie_name) {
  let name = cookie_name + '='
  let decodedCookie = decodeURIComponent(document.cookie)
  let cookieArray = decodedCookie.split(';')
  for (let i = 0; i < cookieArray.length; i++) {
    let cookie = cookieArray[i]
    while (cookie.charAt(0) === ' ') {
      cookie = cookie.substring(1)
    }
    if (cookie.indexOf(name) === 0) {
      return cookie.substring(name.length, cookie.length)
    }
  }
  return null
}

function removeCookie(cookie_name) {
  const cookieName = encodeURIComponent(cookie_name)
  document.cookie = cookieName + '=; max-age=-1;'
}
export const [getLoggedInEmail, setLoggedInEmail] = createCookie('loggedInEmail')
export const [getAuthToken, setAuthToken] = createCookie('authToken')
