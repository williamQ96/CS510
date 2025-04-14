## 👻 Gameplay Feature Summary

### ✅ Dot Product
We use the dot product to calculate the angle between the ghost’s forward direction and the vector pointing toward the player.  
If the angle is within a certain range (i.e., the player is in front of the ghost), the ghost is able to see and chase the player.

### ✅ Linear Interpolation (Lerp)
Linear interpolation is used to smoothly rotate the ghost when the player moves behind it,  
creating a more natural turning behavior instead of an abrupt change in direction.

### ✅ Particle Effect
When the player holds **Left Shift** to sprint, the character’s movement speed increases by **3x**.  
During this sprint, a **particle effect** is triggered and follows the character, enhancing the sense of speed.

### ✅ Sound Effect
When the player is caught by a ghost, a **Sekiro-style death sound** is played,  
accompanied by a visual inspired by Sekiro’s death screen.

---

## 👥 Team Contributions

- **Jou-chi**
  - Implemented the **dot product** logic for ghost vision
  - Implemented **linear interpolation** for ghost turning behavior

- **William**
  - Created and configured the **sprint particle effect**
  - Implemented the **sound effect** triggered on player capture
